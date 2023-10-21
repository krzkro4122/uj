# https://rasa.com/docs/rasa/core/actions/#custom-actions/

import calendar
import json

from typing import Any, Text, Dict, List
from datetime import date, datetime

from rasa_sdk import Action, Tracker, FormValidationAction
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet


class ActionShowOpeningHours(Action):
    def name(self) -> Text:
        return "action_show_opening_hours"

    def __init__(self):
        self.opening_hours: dict
        with open("./opening_hours.json", "r") as f:
            self.opening_hours = json.load(f)
        super().__init__()

    def check_if_open_now(self) -> str:

        day = calendar.day_name[date.today().weekday()]

        if (day == "Sunday"):
            return "The restaurant is CLOSED (because it's Sunday)! :/"

        open_hour = self.opening_hours['items'][day]["open"]
        close_hour = self.opening_hours['items'][day]["close"]
        current_hour = datetime.now().hour

        if abs(open_hour - current_hour) < 2:
            return f"It's CLOSED but it's opening soon (at {open_hour} AM)"
        if abs(close_hour - current_hour) < 2:
            return f"It's OPEN but it's closing soon (at {close_hour})"
        if open_hour < current_hour < close_hour:
            return "The restaurant is OPEN!"

        return "The restaurant is CLOSED :/"

    def format_schedule(self) -> List[str]:
        return [
            f"| {weekday: >12}: {self.opening_hours['items'][weekday]['open']} - {self.opening_hours['items'][weekday]['close']}\t|"
            if weekday != "Sunday" else f"| {weekday: >12}: closed\t|"
            for weekday in self.opening_hours['items']
        ]

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        dispatcher.utter_message(" --- Our opening hours ---")
        dispatcher.utter_message("\n".join(self.format_schedule()))
        dispatcher.utter_message(" -------------------------")

        message = self.check_if_open_now()
        dispatcher.utter_message(message)

        if 'close' in message:
            status = False
        else:
            status = True

        return [SlotSet("open_now", status)]


class ActionShowMenu(Action):
    def name(self) -> Text:
        return "action_show_menu"

    def __init__(self):
        self.menu: dict = self.read_menu()
        super().__init__()

    @staticmethod
    def read_menu() -> Dict:
        with open("./menu.json", "r") as f:
            return json.load(f)

    def format_menu(self):
        return [
            f"| {entry['name']: >19} - {entry['price']: >7} PLN - (ready in ~{entry['preparation_time']}h)\t|"
            for entry in self.menu["items"]
        ]

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        dispatcher.utter_message(" ------------------- Here's our menu: ------------------ ")
        dispatcher.utter_message("\n".join(self.format_menu()))
        dispatcher.utter_message(" ------------------------------------------------------- ")

        return []


class ActionConfirmOrder(Action):
    def name(self) -> Text:
        return "action_confirm_order"

    def __init__(self):
        self.menu: dict = self.read_menu()
        super().__init__()

    @staticmethod
    def read_menu() -> Dict:
        with open("./menu.json", "r") as f:
            return json.load(f)

    def get_total_prep_time_and_price(self, meals):
        prep_time: float = 0.0
        price: int = 0
        for meal_name in meals:

            # Find the current meal
            for meal in self.menu.get('items'):
                if meal.get('name') == meal_name:
                    temp_time = meal.get('preparation_time')
                    price += meal.get('price')

            if temp_time:
                prep_time = temp_time if temp_time > prep_time else prep_time

        return prep_time, price

    @staticmethod
    def order_entry2price(menu, order_entry):
        # Find the current meal
        for meal in menu.get('items'):
            if meal.get('name') in order_entry:
                return meal.get('price')

    def format_order_summary(self):
        if len(self.order) == 0:
            raise Exception("No meals were ordered! (Or i am just that bad at python lol")

        output: List[str]
        for entry in self.order:
            price = self.order_entry2price(self.menu, entry)
            output.append(f" - {entry:<14}\t{price}PLN")
        return "\n".join(output)

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        ordered_meals = tracker.get_slot('ordered_meals')
        order = tracker.get_slot('order')
        prep_time, price = self.get_total_prep_time_and_price(ordered_meals)

        dispatcher.utter_message("Order Summary: ")
        dispatcher.utter_message(self.format_order_summary())
        dispatcher.utter_message("Is the order correct? (yes/no)")

        return []


class ActionValidateOrderForm(FormValidationAction):
    def name(self) -> Text:
        return "validate_order_form"

    def __init__(self):
        self.menu: dict = self.read_menu()
        self.meals: List[str] = self.get_meal_names()
        self.order: List[str] = []
        self.ordered_meals: List[str] = []
        super().__init__()

    @staticmethod
    def read_menu() -> Dict:
        with open("./menu.json", "r") as f:
            return json.load(f)

    def get_meal_names(self) -> List[str]:
        return [meal.get('name') for meal in self.menu.get('items')]

    def get_meal_name(self, order_entry: str) -> List[str]:
        for meal in self.meals:
            if meal in order_entry:
                return meal

    def validate_order(
        self,
        slot_value: Any,
        dispatcher: CollectingDispatcher,
        tracker: Tracker,
        domain: Dict[Text, Any],
    ) -> Dict[Text, Any]:
        """Validate order."""

        print("I am in the form!")

        if slot_value.lower() in ['done']:
            tracker.slots.update({'ordered_meals': self.ordered_meals})
            print("ordered_meals slot: ", tracker.get_slot('ordered_meals'))
            print("order slot: ", tracker.get_slot('order'))
            print("Quitting the order form...")
            return {'order': self.order}

        elif slot_value.lower() in self.meals:
            # validation succeeded, add the wanted meal to the order but not the slot
            self.order.append(slot_value)
            self.ordered_meals.append(self.get_meal_name(slot_value.lower()))

            print(f'order: {self.order}')
            print(f'ordered_meals: {self.ordered_meals}')

            return {"order": self.order}
        else:
            # validation failed, slot set to None so that the
            # user will be asked for the slot again
            return {"order": None}
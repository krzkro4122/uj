import json

class Test:
    def __init__(self):
        self.menu: dict
        with open("./assistant/menu.json", "r") as f:
            self.menu = json.load(f)

        super().__init__()

    def get_prep_time(self, meals):

        prep_time: float = 0.0

        for meal_name in meals:

            # Find the current meal
            for meal in self.menu.get('items'):
                print(meal)
                if meal.get('name') == meal_name:
                    temp_time = meal.get('preparation_time')

            if temp_time:
                prep_time = temp_time if temp_time > prep_time else prep_time

        return prep_time


x = Test().get_prep_time(["Tiramisu", "Hot-dog"])
print(x)
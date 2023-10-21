
import requests


def get_intent(sentence):
    url = "http://localhost:5005/model/parse"
    payload = {'text': sentence}
    response = requests.post(url, json=payload)
    return response.json()


def main():
    intent = get_intent("I'd like to order the Lasagne")
    print(intent)


if __name__ == "__main__":
    main()
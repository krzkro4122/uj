#!/bin/bash

function init() {
    mkdir assistant
    cd assistant
    PYTHONIOENCODING='utf8'
    rasa init --no-prompt
}

# init
rasa train
rasa run --enable-api --endpoints endpoints.yml
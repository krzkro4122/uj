lapis = require "lapis"

import respond_to from require "lapis.application"
import json_params from require "lapis.application"
import Model from require "lapis.db.model"


class Potatoes extends Model
  @relations: {
    {"species", has_one: "Species"}
  }
  get_name: =>
    @name


class Species extends Model
  get_name: =>
    @name
  @relations: {
    {"potatoes", has_many: "Potatoes"}
  }


class App extends lapis.Application

  "/potatoes": respond_to {

    GET: =>
      json: Potatoes\select ""

    POST: json_params =>
      Potatoes\create @params
      json: @params

  }

  "/species": respond_to {

    GET: =>
      json: Species\select ""

    POST: json_params =>
      Species\create @params
      json: @params

  }

  "/potatoes/:id": respond_to {

    before: =>
      @potatoes = Potatoes\find @params.id
      @write status: 404, "Not Found" unless @potatoes

    GET: =>
      json: @potatoes

    PUT: json_params =>
      @potatoes\update @params
      json: @params

    DELETE: =>
      @potatoes\delete!
      json:
        message: "Deletetion operation finished."

  }

  "/species/:id": respond_to {

    before: =>
      @species = Species\find @params.id
      @write status: 404, "Not Found" unless @species

    GET: =>
      json: Potatoes\select "where SpeciesID = ?", @params.id

    PUT: json_params =>
      @species\update @params
      json: @params

    DELETE: =>
      @species\delete!
      json:
        message: "Deletion operation finished."

  }

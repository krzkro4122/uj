local lapis = require("lapis")
local respond_to
respond_to = require("lapis.application").respond_to
local json_params
json_params = require("lapis.application").json_params
local Model
Model = require("lapis.db.model").Model
local Potatoes
do
  local _class_0
  local _parent_0 = Model
  local _base_0 = {
    get_name = function(self)
      return self.name
    end
  }
  _base_0.__index = _base_0
  setmetatable(_base_0, _parent_0.__base)
  _class_0 = setmetatable({
    __init = function(self, ...)
      return _class_0.__parent.__init(self, ...)
    end,
    __base = _base_0,
    __name = "Potatoes",
    __parent = _parent_0
  }, {
    __index = function(cls, name)
      local val = rawget(_base_0, name)
      if val == nil then
        local parent = rawget(cls, "__parent")
        if parent then
          return parent[name]
        end
      else
        return val
      end
    end,
    __call = function(cls, ...)
      local _self_0 = setmetatable({}, _base_0)
      cls.__init(_self_0, ...)
      return _self_0
    end
  })
  _base_0.__class = _class_0
  local self = _class_0
  self.relations = {
    {
      "species",
      has_one = "Species"
    }
  }
  if _parent_0.__inherited then
    _parent_0.__inherited(_parent_0, _class_0)
  end
  Potatoes = _class_0
end
local Species
do
  local _class_0
  local _parent_0 = Model
  local _base_0 = {
    get_name = function(self)
      return self.name
    end
  }
  _base_0.__index = _base_0
  setmetatable(_base_0, _parent_0.__base)
  _class_0 = setmetatable({
    __init = function(self, ...)
      return _class_0.__parent.__init(self, ...)
    end,
    __base = _base_0,
    __name = "Species",
    __parent = _parent_0
  }, {
    __index = function(cls, name)
      local val = rawget(_base_0, name)
      if val == nil then
        local parent = rawget(cls, "__parent")
        if parent then
          return parent[name]
        end
      else
        return val
      end
    end,
    __call = function(cls, ...)
      local _self_0 = setmetatable({}, _base_0)
      cls.__init(_self_0, ...)
      return _self_0
    end
  })
  _base_0.__class = _class_0
  local self = _class_0
  self.relations = {
    {
      "potatoes",
      has_many = "Potatoes"
    }
  }
  if _parent_0.__inherited then
    _parent_0.__inherited(_parent_0, _class_0)
  end
  Species = _class_0
end
local App
do
  local _class_0
  local _parent_0 = lapis.Application
  local _base_0 = {
    ["/potatoes"] = respond_to({
      GET = function(self)
        return {
          json = Potatoes:select("")
        }
      end,
      POST = json_params(function(self)
        Potatoes:create(self.params)
        return {
          json = self.params
        }
      end)
    }),
    ["/species"] = respond_to({
      GET = function(self)
        return {
          json = Species:select("")
        }
      end,
      POST = json_params(function(self)
        Species:create(self.params)
        return {
          json = self.params
        }
      end)
    }),
    ["/potatoes/:id"] = respond_to({
      before = function(self)
        self.potatoes = Potatoes:find(self.params.id)
        if not (self.potatoes) then
          return self:write({
            status = 404
          }, "Not Found")
        end
      end,
      GET = function(self)
        return {
          json = self.potatoes
        }
      end,
      PUT = json_params(function(self)
        self.potatoes:update(self.params)
        return {
          json = self.params
        }
      end),
      DELETE = function(self)
        self.potatoes:delete()
        return {
          json = {
            message = "Deletetion operation finished."
          }
        }
      end
    }),
    ["/species/:id"] = respond_to({
      before = function(self)
        self.species = Species:find(self.params.id)
        if not (self.species) then
          return self:write({
            status = 404
          }, "Not Found")
        end
      end,
      GET = function(self)
        return {
          json = Potatoes:select("where SpeciesID = ?", self.params.id)
        }
      end,
      PUT = json_params(function(self)
        self.species:update(self.params)
        return {
          json = self.params
        }
      end),
      DELETE = function(self)
        self.species:delete()
        return {
          json = {
            message = "Deletion operation finished."
          }
        }
      end
    })
  }
  _base_0.__index = _base_0
  setmetatable(_base_0, _parent_0.__base)
  _class_0 = setmetatable({
    __init = function(self, ...)
      return _class_0.__parent.__init(self, ...)
    end,
    __base = _base_0,
    __name = "App",
    __parent = _parent_0
  }, {
    __index = function(cls, name)
      local val = rawget(_base_0, name)
      if val == nil then
        local parent = rawget(cls, "__parent")
        if parent then
          return parent[name]
        end
      else
        return val
      end
    end,
    __call = function(cls, ...)
      local _self_0 = setmetatable({}, _base_0)
      cls.__init(_self_0, ...)
      return _self_0
    end
  })
  _base_0.__class = _class_0
  if _parent_0.__inherited then
    _parent_0.__inherited(_parent_0, _class_0)
  end
  App = _class_0
  return _class_0
end

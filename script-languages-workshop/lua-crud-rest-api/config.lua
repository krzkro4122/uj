-- config.lua
local config = require("lapis.config")

config("development", {
  mysql = {
    host = "127.0.0.1",
    user = "ziemniak",
    password = "enjoyer",
    database = "ziemniaki"
  },
  server = 'cqueues',
  port = 8081,
  code_cache = 'on',
})
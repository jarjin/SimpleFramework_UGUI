require "pblua/login_pb"
require "pbc/protobuf"

local lpeg = require "lpeg"

local json = require "cjson"
local util = require "cjson.util"

require "logic/luaclass"
require "common/define"
require "common/functions"

--管理器--
local game;	
local this;
GameManager = {};

function GameManager.LuaScriptPanel()
    this = GameManager;
	return 'Prompt', 'Message';
end

function GameManager.Awake()
    --warn('Awake--->>>');
end

--启动事件--
function GameManager.Start()
	--warn('Start--->>>');
end

--初始化完成，发送链接服务器信息--
function GameManager.OnInitOK()
	warn('OnInitOK--->>>');
	createPanel("Prompt");

    Const.SocketPort = 2012;
    Const.SocketAddress = "127.0.0.1";
    ioo.networkManager:SendConnect();

    this.test_class_func();
    this.test_pblua_func();
    this.test_cjson_func();
    this.test_pbc_func();
    this.test_lpeg_func();
end

function GameManager.test_lpeg_func()
    warn("test_lpeg_func-------->>");
    -- matches a word followed by end-of-string
    local p = lpeg.R"az"^1 * -1

    print(p:match("hello"))        --> 6
    print(lpeg.match(p, "hello"))  --> 6
    print(p:match("1 hello"))      --> nil
end

--测试lua类--
function GameManager.test_class_func()
    luaclass:New(10, 20):test();
end

--测试pblua--
function GameManager.test_pblua_func()
    local login = login_pb.LoginRequest();
    login.id = 2000;
    login.name = 'game';
    login.email = 'jarjin@163.com';
    
    local msg = login:SerializeToString();
    LuaHelper.OnCallLuaFunc(msg, this.OnPbluaCall);
end

--pblua callback--
function GameManager.OnPbluaCall(data)
    local msg = login_pb.LoginRequest();
    msg:ParseFromString(data);
    print(msg);
    print(msg.id..' '..msg.name);
end

--测试pbc--
function GameManager.test_pbc_func()
    local path = Util.DataPath.."lua/pbc/addressbook.pb";
    log('io.open--->>>'..path);

    local addr = io.open(path, "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)

    local addressbook = {
        name = "Alice",
        id = 12345,
        phone = {
            { number = "1301234567" },
            { number = "87654321", type = "WORK" },
        }
    }
    local code = protobuf.encode("tutorial.Person", addressbook)
    LuaHelper.OnCallLuaFunc(code, this.OnPbcCall)
end

--pbc callback--
function GameManager.OnPbcCall(data)
    local path = Util.DataPath.."lua/pbc/addressbook.pb";

    local addr = io.open(path, "rb")
    local buffer = addr:read "*a"
    addr:close()
    protobuf.register(buffer)
    local decode = protobuf.decode("tutorial.Person" , data)

    print(decode.name)
    print(decode.id)
    for _,v in ipairs(decode.phone) do
        print("\t"..v.number, v.type)
    end
end

--测试cjson--
function GameManager.test_cjson_func()
    local path = Util.DataPath.."lua/cjson/example2.json";
    local text = util.file_load(path);
    LuaHelper.OnJsonCallFunc(text, this.OnJsonCall);
end

--cjson callback--
function GameManager.OnJsonCall(data)
    local obj = json.decode(data);
    print(obj['menu']['id']);
end

--销毁--
function GameManager.OnDestroy()
	--warn('OnDestroy--->>>');
end

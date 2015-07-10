
require "common/define"
require "common/protocal"
require "common/functions"
Event = require 'events'

Network = {};
local this = Network;

local transform;
local gameObject;
local islogging = false;

function Network.Start() 
    warn("Network.Start!!");
    Event.AddListener(Connect, this.OnConnect); 
    Event.AddListener(Login, this.OnLogin); 
    Event.AddListener(Exception, this.OnException); 
    Event.AddListener(Disconnect, this.OnDisconnect); 
end

--Socket消息--
function Network.OnSocket(key, buffer)
    Event.Brocast(tostring(key), data);
end

--当连接建立时--
function Network.OnConnect() 
    warn("Game Server connected!!");
end

--异常断线--
function Network.OnException() 
    islogging = false; 
    NetManager:SendConnect();
   	error("OnException------->>>>");
end

--连接中断，或者被踢掉--
function Network.OnDisconnect() 
    islogging = false; 
    error("OnDisconnect------->>>>");
end

--当登录时--
function Network.OnLogin(buffer) 
    warn('OnLogin----------->>>');
    --createPanel("Message"); --Lua里创建面板
    local ctrl = CtrlManager.GetCtrl(CtrlName.Message);
    if ctrl ~= nil then
        ctrl:Awake();
    end
end

--卸载网络监听--
function Network.Unload()
    Event.RemoveListener(Connect);
    Event.RemoveListener(Login);
    Event.RemoveListener(Exception);
    Event.RemoveListener(Disconnect);
    warn('Unload Network...');
end
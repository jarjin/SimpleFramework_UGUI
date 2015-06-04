require "common/functions"

MessagePanel = {};
local this = MessagePanel;

local message;
local transform;
local gameObject;

--启动事件--发送登录请求--
function MessagePanel.Start()
	warn("Start lua--->>"..this.gameObject.name);

	message = this.gameObject:GetComponent('BaseLua');
	message:AddClick('Button', this.OnClick);
end

--单击事件--
function MessagePanel.OnClick(go)
	destroy(this.gameObject);
end


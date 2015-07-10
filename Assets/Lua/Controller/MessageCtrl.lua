
MessageCtrl = {};
local this = MessageCtrl;

local message;
local transform;
local gameObject;

--构建函数--
function MessageCtrl.New()
	warn("MessageCtrl.New--->>");
	return this;
end

function MessageCtrl.Awake()
	warn("MessageCtrl.Awake--->>");
	PanelManager:CreatePanel('Message', this.OnCreate);
end

--启动事件--
function MessageCtrl.OnCreate(obj)
	gameObject = obj;

	message = gameObject:GetComponent('LuaBehaviour');
	message:AddClick(MessagePanel.btnClose, this.OnClick);

	warn("Start lua--->>"..gameObject.name);
end

--单击事件--
function MessageCtrl.OnClick(go)
	destroy(gameObject);
end

--关闭事件--
function MessageCtrl.Close()
	PanelManager:ClosePanel(CtrlName.Message);
end
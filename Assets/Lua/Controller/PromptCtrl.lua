require "Common/define"

PromptCtrl = {};
local this = PromptCtrl;

local panel;
local prompt;
local transform;
local gameObject;

--构建函数--
function PromptCtrl.New()
	warn("PromptCtrl.New--->>");
	return this;
end

function PromptCtrl.Awake()
	warn("PromptCtrl.Awake--->>");
	PanelManager:CreatePanel('Prompt', this.OnCreate);
end

--启动事件--
function PromptCtrl.OnCreate(obj)
	gameObject = obj;
	transform = obj.transform;

	panel = transform:GetComponent('UIPanel');
	prompt = transform:GetComponent('LuaBehaviour');
	warn("Start lua--->>"..gameObject.name);

	prompt:AddClick(PromptPanel.btnOpen, this.OnClick);
	ResManager:LoadAsset('prompt', 'PromptItem', this.InitPanel);
end

--初始化面板--
function PromptCtrl.InitPanel(prefab)
	local count = 100; 
	local parent = PromptPanel.gridParent;
	for i = 1, count do
		local go = newobject(prefab);
		go.name = 'Item'..tostring(i);
		go.transform:SetParent(parent);
		go.transform.localScale = Vector3.one;
		go.transform.localPosition = Vector3.zero;

	    local label = go.transform:FindChild('Text');
	    label:GetComponent('Text').text = tostring(i);
	end
	local rtTrans = parent:GetComponent("RectTransform");
	local rowNum = count / 4;
	if count % 4 > 0 then
		rowNum = toInt(rowNum + 1);
	end
	local size = rtTrans.sizeDelta;
	size.y = rowNum * 100 + (rowNum - 1) * 50;
	rtTrans.sizeDelta = size;

	local position = rtTrans.localPosition;
	position.y = -(size.y / 2);
	rtTrans.localPosition = position;
end

--单击事件--
function PromptCtrl.OnClick(go)
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteString("ffff我的ffffQ靈uuu");
    buffer:WriteInt(200);
    NetManager:SendMessage(buffer);
	warn("OnClick---->>>"..go.name);
end

--关闭事件--
function PromptCtrl.Close()
	PanelManager:ClosePanel(CtrlName.Prompt);
end
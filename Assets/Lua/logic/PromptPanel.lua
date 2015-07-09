require "common/functions"

local trans;
local prompt;
local gameObject;

local this;
PromptPanel = {};

--启动事件--
function PromptPanel.Start()
	this = PromptPanel;
	gameObject = find("PromptPanel");
	trans = gameObject.transform;
	prompt = gameObject:GetComponent('LuaBehaviour');

	prompt:AddClick('Open', this.OnClick);
	ResManager:LoadAsset('prompt', 'PromptItem', this.InitPanel);
	warn("Start lua--->>"..gameObject.name);
end

--初始化面板--
function PromptPanel.InitPanel(prefab)
	local count = 100; 
	local parent = trans:Find('ScrollView/Grid');
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
function PromptPanel.OnClick(obj)
    local buffer = ByteBuffer.New();
    buffer:WriteShort(Login);
    buffer:WriteString("ffff我的ffffQ靈uuu");
    buffer:WriteInt(200);
    NetManager:SendMessage(buffer);
	warn("OnClick---->>>"..obj.name);
end
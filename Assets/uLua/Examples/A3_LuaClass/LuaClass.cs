using UnityEngine;
using System.Collections;

public class LuaClass : MonoBehaviour {
    const string source = @"
        Account = { balance = 0 };
        
        function Account:new(o)    
            o = o or {};
            setmetatable(o, { __index = self });     
            return o;  
        end  
  
        function Account.deposit(self, v)  
            self.balance = self.balance + v;  
        end  
  
        function Account:withdraw(v)  
            if (v) > self.balance then error 'insufficient funds'; end  
            self.balance = self.balance - v;  
        end 

        SpecialAccount = Account:new();

        function SpecialAccount:withdraw(v)  
            if v - self.balance >= self:getLimit() then  
                error 'insufficient funds';  
            end  
            self.balance = self.balance - v;  
        end  
  
        function SpecialAccount.getLimit(self)  
            return self.limit or 0;  
        end  

        s = SpecialAccount:new{ limit = 1000 };
        print(s.balance);  
        s:deposit(100.00);

        print (s.limit);  
        print (s.getLimit(s))  
        print (s.balance)  
    ";

	// Use this for initialization
	void Start () {
        LuaScriptMgr mgr = new LuaScriptMgr();
        mgr.Start();
        mgr.lua.DoString(source);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

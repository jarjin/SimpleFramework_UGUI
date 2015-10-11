using UnityEngine;
using System.Collections;
using LuaInterface;

/// <summary>
/// 反射调用方式是不推荐使用的，因为效率慢，推荐的是使用wrap的去反射模式，
/// 这里还继续保留下来的原因在于，在某些特定环境下，反射还是有用途的，
/// 去反射最大的弊端在于提前需要把C#的类导入到Lua中，如果上线了发现有些类没有导入，
/// 反射就可以通过临时的调用未wrap的类，进行使用，当大版本更新时，再将此类加入wrap，
/// 这时候反射就是解决这种情况出现，所以概率小，1%的可能性，但并不代表不存在。
/// </summary>
public class CreateGameObject01 : MonoBehaviour {
    
    private string script = @"
            luanet.load_assembly('UnityEngine')
            GameObject = luanet.import_type('UnityEngine.GameObject')        
	        ParticleSystem = luanet.import_type('UnityEngine.ParticleSystem')         
   
            local newGameObj = GameObject('NewObj')
            newGameObj:AddComponent(luanet.ctype(ParticleSystem))
        ";

	//反射调用
	void Start () {
        LuaState lua = new LuaState();
        lua.DoString(script);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

public interface IInput
{
    public float GetAxis(string axisName);
    public float GetAxisRaw(string axisName);
    public bool GetButton(string buttonName);
    public bool GetButtonDown(string buttonName);
    public bool GetButtonUp(string buttonName);
    public string[] GetJoystickNames();
    public bool GetKey(string name);
    public bool GetKeyDown(string name);
    public bool GetKeyUp(string name);
    public bool GetMouseButton(int button);
    public bool GetMouseButtonDown(int button);
    public bool GetMouseButtonUp(int button);
    public bool IsJoystickPreconfigured(string joystickName);
};


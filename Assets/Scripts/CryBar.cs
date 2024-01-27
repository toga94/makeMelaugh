struct CryBar {
    float minValue;
    float maxValue;
    float currentValue;
    public float GetValue
    {
        get {
            return currentValue;
        }
    }
    public bool isWon
    {
        get
        {
            return currentValue <= minValue;
        }
    }
    public bool isLose
    {
        get
        {
            return currentValue >= maxValue;
        }
    }

    public void MakeMoreCry(float value) {
         currentValue += value;
    }
    public void MakeMoreLaugh(float value) {
         currentValue -= value;
    }

    public float RunTimer(float deltatime, float multiply = 1) {
        currentValue += multiply * deltatime;
        return currentValue / maxValue; 
    }
    public CryBar(float maxValue, float currentValue)
    {
        this.minValue = -maxValue;
        this.maxValue = maxValue;
        currentValue = minValue / maxValue;
        this.currentValue = currentValue;
    }
}

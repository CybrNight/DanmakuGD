using System;

public static class MLConfig{

    static MLConfig(){
        FixedTimeStep = (100.0f / 6000.0f);
    }

    public static float FixedTimeStep { get; set; } = (100.0f / 6000.0f);
}


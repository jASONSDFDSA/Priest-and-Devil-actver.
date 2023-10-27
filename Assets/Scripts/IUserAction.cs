using System;

public interface IUserAction
{
    void Init();
    void GameOver();
    void moveOnBoat(int index, bool isPriest);
    void boatMov();
    bool Judge();
}


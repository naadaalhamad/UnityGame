using System;

[Serializable]
public class DailyTask
{
    public string description; 
    public string taskType;    
    public int targetAmount;  
    public int xpReward;      
    public int coinReward;    

   
    public int currentAmount; 
    public bool completed;    
}
package com.example.lenovo.bustravel;

public class BusFlight {


   int id;
   String citydeparture;
   String cityarrival;
   String timedeparture;
   String timearrival;
   String company;
  String p;

   public BusFlight(int id, String citydeparture, String cityarrival, String timedeparture, String timearrival,
                    String company, String p) {
       this.id = id;
       this.citydeparture = citydeparture;
       this.cityarrival = cityarrival;
       this.timedeparture = timedeparture;
       this.timearrival = timearrival;
       this.company = company;
       this.p = p;
   }
}

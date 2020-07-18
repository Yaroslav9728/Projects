package com.example.lenovo.bustravel;

public class Ticket {
    int id;
    String citydep;
    String cityarr;
    String timedep;
    String timearr;
    double price;
    String date;
    String name;

    public  Ticket(int _id, String _citydep, String _cityarr, String _timedep, String _timearr, double _price, String _date, String _name) {
        this.id = _id;
        this.cityarr = _cityarr;
        this.citydep = _citydep;
        this.timearr = _timearr;
        this.timedep = _timedep;
        this.price = _price;
        this.date = _date;
        this.name = _name;
    }

}

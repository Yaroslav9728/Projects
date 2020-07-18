package com.example.lenovo.bustravel;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.support.v7.widget.CardView;
import android.support.v7.widget.DividerItemDecoration;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.View;
import android.widget.Toast;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.List;

public class FoundFlightsActivity extends AppCompatActivity  {
    String citydep;
     String cityarr;
     String comp;
     String timeDepature;
     String timeArrival;
     String p;
     int id;
     BusFlight busFlight;

List<BusFlight> busFlights;
RecyclerView view;
BusFlightAdapter adapter;
Bundle bundle;
String json;


    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_found_flights);
         bundle = getIntent().getExtras();

         if(bundle != null) {
              json = bundle.getString("flights");
         }
         else {
             startActivity(new Intent(FoundFlightsActivity.this, TicketActivity.class));
         }
             busFlights = new ArrayList<>();
             DividerItemDecoration dividerItemDecoration = new DividerItemDecoration(getApplicationContext(), DividerItemDecoration.VERTICAL);

view = findViewById(R.id.rv);
     view.addItemDecoration(dividerItemDecoration);

        LinearLayoutManager llm = new LinearLayoutManager(this);
        view.setLayoutManager(llm);

        if(json == null) {
            startActivity(new Intent(FoundFlightsActivity.this, TicketActivity.class));
        }
            if (json != null) {
                try {
                JSONObject object = new JSONObject(json);
                JSONArray flights = object.getJSONArray("BusFlight");


                for (int i = 0; i < flights.length(); i++) {

                    JSONObject flight = flights.getJSONObject(i);

                    id = flight.getInt("ID");
                    citydep = flight.getString("CityDeparture");
                    cityarr = flight.getString("CityArrival");

                    timeDepature = flight.getString("TimeDeparture");
                    timeArrival = flight.getString("TimeArrival");
                    comp = flight.getString("Name");
                    p = flight.getString("Price");


                    busFlights.add(new BusFlight(id, citydep, cityarr, timeDepature, timeArrival, comp, p));


                }
                adapter = new BusFlightAdapter(busFlights);
                view.setAdapter(adapter);


            } catch(JSONException e){
                e.printStackTrace();
            }
        }
    }
}

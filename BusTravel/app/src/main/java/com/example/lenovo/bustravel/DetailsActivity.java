package com.example.lenovo.bustravel;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class DetailsActivity extends AppCompatActivity {
TextView cd,ca,td,ta,compa,p,d,dat,s, bm, bmn;
String stringID;
User user;
int idf;
int id;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_details);

        user = User.getInstance();

        cd = findViewById(R.id.CD);
        ca = findViewById(R.id.CA);
        td = findViewById(R.id.TD);
        ta = findViewById(R.id.TA);
        compa = findViewById(R.id.compa);
        p = findViewById(R.id.P);
        d = findViewById(R.id.D);
        dat = findViewById(R.id.date);
        s = findViewById(R.id.busCapacity);
        bm = findViewById(R.id.BusLabel);
        bmn = findViewById(R.id.busModel);

        Intent bundle = getIntent();

        if(bundle != null) {
            id = bundle.getExtras().getInt("id");
            stringID = Integer.toString(id);
        }
        else {
            super.onBackPressed();
        }

        String url = "https://yaroslav9728.000webhostapp.com/FlightDetails.php";

        StringRequest postRequest = new StringRequest(Request.Method.POST, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        // response

                        try {
                            JSONObject object = new JSONObject(response);
                            int status = object.getInt("state");
                            //Check if user got registered successfully
                            if (status == 0) {
                                //Set the user session
                                JSONObject flightDetails = object.getJSONObject("FlightDetails");

                                idf = flightDetails.getInt("ID");

                                String citydep = flightDetails.getString("CityDeparture");
                                String cityarr = flightDetails.getString("CityArrival");
                                String timedep = flightDetails.getString("TimeDeparture");
                                String timearr = flightDetails.getString("TimeArrival");
                                String company = flightDetails.getString("Company");
                                String seats = flightDetails.getString("Seats");
                                String date = flightDetails.getString("Date");
                                String distance = flightDetails.getString("Distance");
                                String price = flightDetails.getString("Price");
                                String marka = flightDetails.getString("Marka");
                                String model = flightDetails.getString("ModelName");

                                cd.setText(citydep);
                                ca.setText(cityarr);
                                td.setText(timedep);
                                ta.setText(timearr);
                                compa.setText(company);
                                s.setText(seats);
                                dat.setText(date);
                                d.setText(distance);
                                p.setText(price);
                                bm.setText(marka);
                                bmn.setText(model);


                            } else if(status == 1) {
                                String message = object.getString("message");
                                Toast.makeText(DetailsActivity.this,
                                        message, Toast.LENGTH_LONG).show();

                            }
                            else {
                                String message = object.getString("message");
                                Toast.makeText(DetailsActivity.this,
                                        message, Toast.LENGTH_LONG).show();
                            }

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }


                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // error
                        Toast.makeText(DetailsActivity.this, error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                }
        ) {
            @Override
            protected Map<String, String> getParams() {
                Map<String, String> params = new HashMap<>();
                params.put("ID", stringID);

                return params;
            }

        };


        // Creating RequestQueue.
        RequestQueue requestQueue = Volley.newRequestQueue(DetailsActivity.this);
        requestQueue.add(postRequest);
    }

    public void Map(View view) {
        Intent intent = new Intent(DetailsActivity.this, MapsActivity.class);
        String cdd = cd.getText().toString();
        String caa = ca.getText().toString();

        intent.putExtra("Point1", cdd);
        intent.putExtra("Point2", caa);
        startActivity(intent);
    }

    public void Ticket(View view) {
        RequestQueue requestQueue = Volley.newRequestQueue(DetailsActivity.this);

        String ticket_url = "https://yaroslav9728.000webhostapp.com/InsertTickets.php";

        StringRequest postRequest = new StringRequest(Request.Method.POST, ticket_url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        // response


                        try {
                            JSONObject object = new JSONObject(response);
                            int status = object.getInt("status");
                            //Check if user got registered successfully
                            if (status == 0) {
                                //Set the user session
                                String message = object.getString("message");
                                Toast.makeText(DetailsActivity.this, message, Toast.LENGTH_LONG).show();


                            } else if (status == 1) {
                                String message = object.getString("message");
                                //Display error message if username is already existsing
                                Toast.makeText(DetailsActivity.this, message, Toast.LENGTH_LONG).show();

                            } else {
                                String message = object.getString("message");
                                Toast.makeText(DetailsActivity.this,
                                        message, Toast.LENGTH_LONG).show();

                            }
                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }

                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // error
                        Toast.makeText(DetailsActivity.this, error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                }
        ) {
            @Override
            protected Map<String, String> getParams() {
                Map<String, String> params = new HashMap<>();
                params.put("UserID", Integer.toString(user.getId()));
                params.put("FlightID", Integer.toString(idf));


                return params;
            }

            // Creating RequestQueue.

        };

        requestQueue.add(postRequest);
        Intent intent = new Intent(this, myTicketsActivity.class);
        startActivity(intent);
    }
}

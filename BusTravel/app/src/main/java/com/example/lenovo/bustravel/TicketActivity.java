package com.example.lenovo.bustravel;

import android.annotation.SuppressLint;
import android.app.DatePickerDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.Intent;
import android.graphics.Color;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.text.format.DateUtils;
import android.util.Patterns;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.CalendarView;
import android.widget.DatePicker;
import android.widget.EditText;
import android.widget.FrameLayout;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.Toolbar;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Locale;
import java.util.Map;
import java.util.regex.Pattern;

import static android.text.format.DateUtils.*;
import static android.text.format.DateUtils.FORMAT_SHOW_DATE;


public class TicketActivity extends AppCompatActivity {

    Button btn;
    EditText citydep,cityarr;
    TextView date;

    String ca;
    String cd;
    String selectedDate;

    private final String KEY_EMPTY = "";
    private String Search_url = "https://yaroslav9728.000webhostapp.com/FlightsSearch.php";
    private ProgressDialog pDialog;
    Calendar date2 = Calendar.getInstance();

    @SuppressLint("ResourceType")
    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
          setContentView(R.layout.activity_ticket);
        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);

        User user = User.getInstance();

       btn = findViewById(R.id.button5);
       citydep = findViewById(R.id.editText7);
       cityarr = findViewById(R.id.editText8);
       date = findViewById(R.id.textView2);


        date.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                setDate(v);
            }
        });


       btn.setOnClickListener(new View.OnClickListener() {
           @Override
           public void onClick(View v) {
               cd = citydep.getText().toString().trim();
               ca = cityarr.getText().toString().trim();
               selectedDate = date.getText().toString() ;        //dat = date.get;

             if(validateInputs()) {
                 citydep.setBackgroundColor(Color.GREEN);
                 cityarr.setBackgroundColor(Color.GREEN);
                 SearchFlights();
             }
           }
       });


    }

    public void setDate(View v) {
        new DatePickerDialog(TicketActivity.this, d,
                date2.get(Calendar.YEAR),
                date2.get(Calendar.MONTH),
                date2.get(Calendar.DAY_OF_MONTH))
                .show();
    }

    private void setInitialDateTime() {
        DateFormat df = new SimpleDateFormat("yyyy-MM-dd");

date.setText(df.format(date2.getTime()));
    }

    DatePickerDialog.OnDateSetListener d = new DatePickerDialog.OnDateSetListener() {
        public void onDateSet(DatePicker view, int year, int monthOfYear, int dayOfMonth) {
            date2.set(Calendar.YEAR, year);
            date2.set(Calendar.MONTH, monthOfYear);
            date2.set(Calendar.DAY_OF_MONTH, dayOfMonth);
            setInitialDateTime();
        }
    };

    private void SearchFlights() {

        displayLoader();
        StringRequest postRequest = new StringRequest(Request.Method.POST, Search_url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String response) {
                        // response
                        pDialog.dismiss();
                        try {
                            JSONObject object = new JSONObject(response);

                            int status = object.getInt("state");
                            //Check if user got registered successfully
                            if (status == 0) {
                                //Set the user session
                                loadDashboard(object);

                            } else if(status == 1) {
                                String message = object.getString("message");
                                Toast.makeText( TicketActivity.this,
                                        message, Toast.LENGTH_LONG).show();

                            }
                            else {
                                String message = object.getString("message");
                                Toast.makeText( TicketActivity.this,
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
                        Toast.makeText(TicketActivity.this, error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                }
        ) {
            @Override
            protected Map<String, String> getParams() {
                Map<String, String> params = new HashMap<>();
                params.put("CD", cd);
                params.put("CA", ca);
               params.put("DT", selectedDate);

                return params;
            }

        };


        // Creating RequestQueue.
        RequestQueue requestQueue = Volley.newRequestQueue(TicketActivity.this);
        requestQueue.add(postRequest);

    }

    private void loadDashboard(JSONObject object) {
        Intent intent = new Intent(this, FoundFlightsActivity.class);
        intent.putExtra("flights",object.toString());
        startActivity(intent);
        finish();
    }

    private void displayLoader() {
        pDialog = new ProgressDialog(TicketActivity.this);
        pDialog.setMessage("Searching... Please wait...");
        pDialog.setIndeterminate(false);
        pDialog.setCancelable(false);
        pDialog.show();

    }

    private boolean validateInputs() {
        if (KEY_EMPTY.equals(cd)) {
            citydep.setBackgroundColor(Color.RED);
            citydep.setError("Empty field");
            citydep.requestFocus();

            return false;
        }
        if (KEY_EMPTY.equals(ca)) {
            cityarr.setBackgroundColor(Color.RED);
            cityarr.setError("Empty field");
            cityarr.requestFocus();
            return false;
        }
        return true;
    }

}

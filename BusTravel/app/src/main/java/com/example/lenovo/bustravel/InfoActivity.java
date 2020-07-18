package com.example.lenovo.bustravel;

import android.annotation.SuppressLint;
import android.content.Intent;
import android.icu.text.IDNA;
import android.net.sip.SipSession;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
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

import java.net.UnknownHostException;
import java.util.HashMap;
import java.util.Map;

public class InfoActivity extends AppCompatActivity {
    User user;

    @SuppressLint("ResourceType")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_info);

        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);

        user = User.getInstance();


        TextView view = findViewById(R.id.textView4);


        view.setText(user.getLogin() + '\n' + user.getEmail());

    }

}

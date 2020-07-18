package com.example.lenovo.bustravel;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;

public class ThanksgivingActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_thanksgiving);
        Intent intent = new Intent(this,SignActivity.class);


            startActivity(intent);
            finish();
        }
    }

package com.example.lenovo.bustravel;

import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Patterns;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
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
import java.util.regex.Pattern;

public class RegisterActivity extends AppCompatActivity {

    private boolean validEmail(String email) {
        Pattern pattern = Patterns.EMAIL_ADDRESS;
        return pattern.matcher(email).matches();
    }

    
    private static final String KEY_EMPTY = "";
    private EditText Username;
    private EditText Password;

    private EditText Email;
    private String username;
    private String password;

    private String email;
    private ProgressDialog pDialog;
    private String register_url = "http://yaroslav9728.000webhostapp.com/Register.php";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);

        setContentView(R.layout.activity_register);

        Username = findViewById(R.id.editText2);
        Password = findViewById(R.id.editText5);

        Email = findViewById(R.id.editText6);
        pDialog = new ProgressDialog(this);
        pDialog.setCancelable(false);
        pDialog.setMessage("Sign Up.......");

        Button register = findViewById(R.id.button4);

        //Launch Login screen when Login Button is clicked


        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                //Retrieve the data entered in the edit texts
                username = Username.getText().toString().trim();
                password = Password.getText().toString().trim();

                email = Email.getText().toString().trim();
                if (validateInputs()) {
                    registerUser();
                }

            }
        });

    }
    private void ShowLoadder() {
        if(!pDialog.isShowing()) {
            pDialog.show();
        }
    }
    private void HideLoader() {
        if(pDialog.isShowing()) {
            pDialog.dismiss();
        }
    }

    private void loadDashboard(String message) {
        Intent i = new Intent(getApplicationContext(), ThanksgivingActivity.class);
        Toast.makeText(RegisterActivity.this, message.toString(), Toast.LENGTH_LONG).show();
        startActivity(i);
        finish();

    }

    private void registerUser() {
        RequestQueue requestQueue = Volley.newRequestQueue(RegisterActivity.this);
        ShowLoadder();
        StringRequest postRequest = new StringRequest(Request.Method.POST, register_url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String answer) {
                        // response
                        HideLoader();
                        try {
                            JSONObject object = new JSONObject(answer);
							int status = object.getInt("state");
                            //Check if user got registered successfully
                            if (status == 0) {
                                //Set the user session
                                String message = object.getString("message");
                                Toast.makeText(RegisterActivity.this, message, Toast.LENGTH_LONG).show();
                                loadDashboard(message);

                            } else if (status == 1) {
                                String message = object.getString("message");
                                //Display error message if username is already existsing
                                Toast.makeText(RegisterActivity.this, message, Toast.LENGTH_LONG).show();

                            } else {
                                String message = object.getString("message");
                                Toast.makeText(RegisterActivity.this,
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
                        Toast.makeText(RegisterActivity.this, error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                }
        ) {
            @Override
            protected Map<String, String> getParams() {
                Map<String, String> params = new HashMap<>();
                params.put("Login", username);
                params.put("PasswordUser", password);
                params.put("Email", email);

                return params;
            }

        };

        requestQueue.add(postRequest);
    }
        private boolean validateInputs () {

            if (KEY_EMPTY.equals(username)) {
                Username.setError("Username cannot be empty");
                Username.setBackgroundColor(Color.RED);
                Username.requestFocus();
                return false;
            }
            if (KEY_EMPTY.equals(password)) {
                Password.setError("Password cannot be empty");
                Password.setBackgroundColor(Color.RED);
                Password.requestFocus();
                return false;
            }
            if (KEY_EMPTY.equals(email)) {
                Email.setError("Email cannot be empty");
                Email.setBackgroundColor(Color.RED);
                Email.requestFocus();
                return false;

            }
            if (!validEmail(email)) {
                Email.setError("Email should contain @ symbol");
                Email.setBackgroundColor(Color.RED);
                Email.requestFocus();
                return false;
            }
            return true;
    }
}

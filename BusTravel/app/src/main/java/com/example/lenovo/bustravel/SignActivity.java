package com.example.lenovo.bustravel;

import android.app.ProgressDialog;
import android.content.Intent;
import android.graphics.Color;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
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

import org.acra.ACRA;
import org.acra.ReportingInteractionMode;
import org.acra.annotation.ReportsCrashes;
import org.json.JSONException;
import org.json.JSONObject;

import java.net.UnknownHostException;
import java.util.HashMap;
import java.util.Map;

public class SignActivity extends AppCompatActivity {
    EditText log,pass;
    Button btn;
    User user;
    private static final String KEY_EMPTY = "";

    private String username;
    private String password;
    private ProgressDialog pDialog;
    private String login_url = "http://yaroslav9728.000webhostapp.com/Login.php";


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        user = User.getInstance();
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_sign);

        log = findViewById(R.id.editText);
        pass = findViewById(R.id.editText4);
        btn = findViewById(R.id.button);
        pDialog = new ProgressDialog(this);
        pDialog.setCancelable(false);
        pDialog.setMessage("Sign In.......");

        btn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {

                username = log.getText().toString().trim();
                password = pass.getText().toString().trim();
                if (validateInputs()) {

                        login();


                    }
                }

        });
}


    private void login () {

try {
    ShowLoadder();

    StringRequest postRequest = new StringRequest(Request.Method.POST, login_url,
            new Response.Listener<String>() {
                @Override
                public void onResponse(String response) {
                    // response
                    HideLoader();
                    try {
                        JSONObject object = new JSONObject(response);
                        int status = object.getInt("state");
                        //Check if user got registered successfully
                        if (status == 0) {
                            //Set the user session
                            String message = object.getString("message");
                            JSONObject userJsonObject = object.getJSONObject("User");

                            int id = userJsonObject.getInt("Id");
                            user.setId(id);

                            String name = userJsonObject.getString("Login");
                            user.setLogin(name);

                            String email = userJsonObject.getString("Email");
                            user.setEmail(email);

                            Toast.makeText(SignActivity.this, message, Toast.LENGTH_LONG).show();
                            loadDashboard();

                        } else {
                            String message = object.getString("message");
                            Toast.makeText(SignActivity.this,
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
                    Toast.makeText(SignActivity.this, error.getMessage(), Toast.LENGTH_LONG).show();
                }
            }
    ) {
        @Override
        protected Map<String, String> getParams() {
            Map<String, String> params = new HashMap<>();
            params.put("Login", username);
            params.put("PasswordUser", password);

            return params;
        }

    };


    // Creating RequestQueue.
    RequestQueue requestQueue = Volley.newRequestQueue(SignActivity.this);
    requestQueue.add(postRequest);

    throw new UnknownHostException();

}
catch(UnknownHostException e) {
    e.printStackTrace();
}

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

    private boolean validateInputs() {
        if(KEY_EMPTY.equals(username)){
            log.setError("Username cannot be empty");
            log.setBackgroundColor(Color.RED);
            log.requestFocus();
            return false;
        }
        if(KEY_EMPTY.equals(password)){
            pass.setError("Password cannot be empty");
            pass.setBackgroundColor(Color.RED);
            pass.requestFocus();
            return false;
        }
        return true;
    }

    private void loadDashboard() {
        Intent intent = new Intent(this, MainActivity.class);


        startActivity(intent);
        finish();
    }


}

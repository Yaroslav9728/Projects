package com.example.lenovo.bustravel;

import android.annotation.SuppressLint;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.graphics.pdf.PdfDocument;
import android.net.Uri;
import android.os.Environment;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.Menu;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.FrameLayout;
import android.widget.ListView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.StringRequest;
import com.android.volley.toolbox.Volley;
import com.itextpdf.text.Chapter;
import com.itextpdf.text.Document;
import com.itextpdf.text.Font;
import com.itextpdf.text.FontFactory;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.Paragraph;
import com.itextpdf.text.pdf.PdfBody;
import com.itextpdf.text.pdf.PdfWriter;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Locale;
import java.util.Map;

public class myTicketsActivity extends AppCompatActivity {
ArrayList<Ticket> ticketList;
ListView listView;
ListAdapter adapter;
int ID;
String citydep, cityarr, timeDeparture, timeArrival, date, comp;
double price;
int id;
    @SuppressLint("ResourceType")
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_my_tickets);
        User user = User.getInstance();


        listView = findViewById(R.id.listview);


        String url = "https://yaroslav9728.000webhostapp.com/SelectTickets.php";

       id = user.getId();

        ticketList = new ArrayList<>();

        StringRequest postRequest = new StringRequest(Request.Method.POST, url,
                new Response.Listener<String>() {
                    @Override
                    public void onResponse(String resp) {
                        // response

                        try {
                            JSONObject object = new JSONObject(resp);
                            int status = object.getInt("state");
                            //Check if user got registered successfully
                            if (status == 0) {
                                //Set the user session
                                JSONArray tickets = object.getJSONArray("Tickets");

                                for (int i = 0; i < tickets.length(); i++) {

                                    JSONObject ticket = tickets.getJSONObject(i);

                                    ID  = ticket.getInt("IDTicket");
                                   citydep = ticket.getString("CityDeparture");
                                     cityarr = ticket.getString("CityArrival");

                                     timeDeparture = ticket.getString("TimeDeparture");
                                    timeArrival = ticket.getString("TimeArrival");

                                    price = ticket.getDouble("Price");
                                    date = ticket.getString("date");
                                    comp = ticket.getString("Name");


                                    ticketList.add(new Ticket(ID, citydep, cityarr, timeDeparture, timeArrival, price, date,comp));

                                }

                            } else if(status == 1) {
                                String message = object.getString("message");
                                Toast.makeText(myTicketsActivity.this,
                                        message, Toast.LENGTH_LONG).show();

                            }
                            else {
                                String message = object.getString("message");
                                Toast.makeText(myTicketsActivity.this,
                                        message, Toast.LENGTH_LONG).show();
                            }
                            adapter = new ListAdapter(myTicketsActivity.this, ticketList);
                            listView.setAdapter(adapter);

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }

                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        // error
                        Toast.makeText(myTicketsActivity.this, error.getMessage(), Toast.LENGTH_LONG).show();
                    }
                }
        ) {
            @Override
            protected Map<String, String> getParams() {
                Map<String, String> params = new HashMap<>();
                params.put("IDD", Integer.toString(id));

                return params;
            }

        };


        // Creating RequestQueue.
        RequestQueue requestQueue = Volley.newRequestQueue(myTicketsActivity.this);
        requestQueue.add(postRequest);


        ActionBar actionBar = getSupportActionBar();
        actionBar.setDisplayHomeAsUpEnabled(true);

       listView.setOnItemClickListener(new AdapterView.OnItemClickListener() {
           @Override
           public void onItemClick(AdapterView<?> parent, View view, int position, long id) {

                Ticket ticket = (Ticket) adapter.getItem(position);
                saveToPDF(ticket);

           }
       });
    }

    private void saveToPDF(Ticket ticket) {
        Document mDoc = new Document(PageSize.A4, 50, 50, 50, 50);
        //pdf file name
        String mFileName = new SimpleDateFormat("yyyyMMdd_HHmmss",
                Locale.getDefault()).format(System.currentTimeMillis());
        //pdf file path
        String mFilePath = Environment.getExternalStorageDirectory() + "/" + mFileName + ".pdf";

        try {
            //create instance of PdfWriter class
            PdfWriter.getInstance(mDoc, new FileOutputStream(mFilePath));
            //open the document for writing
            mDoc.open();
            //get text from EditText i.e. mTextEt

           Paragraph title = new Paragraph("Проїзний документ", FontFactory.getFont(FontFactory.TIMES_ITALIC,24));
           title.setAlignment(Paragraph.ALIGN_CENTER);
           title.setSpacingAfter(25);

           mDoc.add(title);

            Paragraph idLabel = new Paragraph("Номер проїзного документа:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(idLabel);

            Paragraph id = new Paragraph(String.valueOf(ticket.id), FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(id);

            Paragraph cdLabel = new Paragraph("Звідки:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(cdLabel);

            Paragraph CD = new Paragraph(ticket.citydep, FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(CD);

            Paragraph caLabel = new Paragraph("Куди:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(caLabel);

            Paragraph CA = new Paragraph(ticket.citydep, FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(CA);


            Paragraph tdLabel = new Paragraph("Час відправлення:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(tdLabel);

            Paragraph TD = new Paragraph(ticket.timedep, FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(TD);

            Paragraph taLabel = new Paragraph("Час прибуття:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(taLabel);

            Paragraph TA = new Paragraph(ticket.citydep, FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(TA);

            Paragraph pLabel = new Paragraph("Вартість:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(pLabel);

            Paragraph P = new Paragraph(String.valueOf(ticket.price), FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(P);

            Paragraph dateLabel = new Paragraph("Дата придбання:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(dateLabel);

            Paragraph DT = new Paragraph(ticket.date, FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(DT);

            Paragraph compLabel = new Paragraph("Перевізник:", FontFactory.getFont(FontFactory.TIMES_BOLD,14));
            mDoc.add(compLabel);

            Paragraph COMP = new Paragraph(ticket.name, FontFactory.getFont(FontFactory.TIMES,14));
            mDoc.add(COMP);


            //close the document
            mDoc.close();
            //show message that file is saved, it will show file name and file path too
            Toast.makeText(this, mFileName +".pdf\nis saved to\n"+ mFilePath, Toast.LENGTH_SHORT).show();

            OpenPdf(mFileName,mFilePath);
        }
        catch (Exception e){
            //if any thing goes wrong causing exception, get and show exception message
            Toast.makeText(this, e.getMessage(), Toast.LENGTH_SHORT).show();
        }
    }

    private void OpenPdf(String mFileName, String mFilePath) {
        File pdfFile = new File(Environment.getExternalStorageDirectory() + "/" + mFilePath + "/" + mFileName);
        Uri path = Uri.fromFile(pdfFile);

        // Setting the intent for pdf reader
        Intent pdfIntent = new Intent(Intent.ACTION_VIEW);
        pdfIntent.setDataAndType(path, "application/pdf");
        pdfIntent.setFlags(Intent.FLAG_ACTIVITY_CLEAR_TOP);

        try {
            startActivity(pdfIntent);
        } catch (ActivityNotFoundException e) {
            Toast.makeText(myTicketsActivity.this, "Can't read pdf file", Toast.LENGTH_SHORT).show();
        }
    }


}

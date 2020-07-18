package com.example.lenovo.bustravel;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

public class ListAdapter extends BaseAdapter {
    private Context activity;
    private LayoutInflater inflater;
    private List<Ticket> DataList;
    Ticket ticket;

    public ListAdapter(Context activity, List<Ticket> tickets) {

        this.activity = activity;
        this.DataList = tickets;
    }

    @Override
    public int getCount() {
        return DataList.size();
    }

    @Override
    public Object getItem(int position) {
        return DataList.get(position);
    }

    @Override
    public long getItemId(int position) {
        return position;
    }


    @Override
    public View getView(int position, View convertView, ViewGroup parent) {
        TextView id, citydep, cityarr, timedep, timearr, price, comp, date;
        View v = convertView;


        if (v == null) {
            inflater = LayoutInflater.from(activity.getApplicationContext());
            v = inflater.inflate(R.layout.list_tickets, null);

        }

            id = v.findViewById(R.id.ticketID);
            citydep = v.findViewById(R.id.citydepa);
            cityarr = v.findViewById(R.id.cityarra);
            timedep = v.findViewById(R.id.timedepa);
            timearr = v.findViewById(R.id.timearra);
            price = v.findViewById(R.id.price);
            date = v.findViewById(R.id.date);
            comp = v.findViewById(R.id.company);


            id.setText("# квитка: " + Integer.toString(DataList.get(position).id));
            citydep.setText("Звідки: " + DataList.get(position).citydep);
            cityarr.setText("Куди: " + DataList.get(position).cityarr);
            timedep.setText("Час відправлення: " + DataList.get(position).timedep);
            timearr.setText("Час прибуття: " + DataList.get(position).timearr);
            price.setText("Ціна: " + Double.toString(DataList.get(position).price));
            date.setText("Дата придбання: " + DataList.get(position).date);
            comp.setText("Перевізник: " + DataList.get(position).name);


        return v;
    }
}

package com.example.lenovo.bustravel;


import android.content.ClipData;
import android.content.Context;
import android.content.Intent;
import android.support.annotation.NonNull;
import android.support.v7.widget.CardView;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.TextView;
import android.widget.Toast;

import java.util.List;

import static java.security.AccessController.getContext;

public class BusFlightAdapter extends RecyclerView.Adapter<BusFlightAdapter.BusViewHolder> {
    List<BusFlight> busFlights;
    int id;

    public BusFlightAdapter( List<BusFlight> busFlights) {
        this.busFlights = busFlights;
    }

    @Override
    public BusViewHolder onCreateViewHolder(ViewGroup viewGroup, final int i) {
        View v = LayoutInflater.from(viewGroup.getContext()).inflate(R.layout.flights_list, viewGroup, false);

        return new BusViewHolder(v);
    }

    @Override
    public void onBindViewHolder(final BusViewHolder busViewHolder, final int i) {
        busViewHolder.cityDep.setText("Пункт відправлення: " + busFlights.get(i).citydeparture.toString());
        busViewHolder.cityArr.setText("Пункт прибуття: "+ busFlights.get(i).cityarrival.toString());
        busViewHolder.company.setText("Перевізник " + busFlights.get(i).company);
        busViewHolder.timeDep.setText("Час відправлення: " + busFlights.get(i).timedeparture.toString());
        busViewHolder.timeArr.setText("Час прибуття: " + busFlights.get(i).timearrival.toString());
        busViewHolder.price.setText("Ціна: " + busFlights.get(i).p);


        busViewHolder.cv.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Toast.makeText(v.getContext(), Integer.toString(busFlights.get(i).id), Toast.LENGTH_SHORT).show();
                Intent intent = new Intent(v.getContext(), DetailsActivity.class);
                intent.putExtra("id",busFlights.get(i).id);
                v.getContext().startActivity(intent);

            }
        });



    }

    @Override
    public void onAttachedToRecyclerView(RecyclerView recyclerView) {
        super.onAttachedToRecyclerView(recyclerView);
    }

    @Override
    public int getItemCount() {
        return busFlights.size();
    }


    public  class BusViewHolder extends RecyclerView.ViewHolder {
        CardView cv;
        TextView cityDep;
        TextView cityArr;
        TextView company;
        TextView timeDep;
        TextView timeArr;
        ClipData.Item item;
        BusFlight bus;
        TextView price;


        public BusViewHolder(View itemView) {
            super(itemView);
            cv = itemView.findViewById(R.id.cv);


            cityDep = (TextView) itemView.findViewById(R.id.cd);
            cityArr = (TextView) itemView.findViewById(R.id.ca);
            company = (TextView) itemView.findViewById(R.id.comp);
            timeDep = (TextView) itemView.findViewById(R.id.td);
            timeArr = (TextView) itemView.findViewById(R.id.ta);
           price = itemView.findViewById(R.id.price);


        }

    }
}

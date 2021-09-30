package com.tris.myapplication;

import android.content.Context;
import android.content.Intent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.bumptech.glide.Glide;
import com.tris.myapplication.Object.Info;
import com.tris.myapplication.Object.ItemClickListener;
import com.tris.myapplication.Object.List;

import java.util.ArrayList;

public class InfoAdapter extends RecyclerView.Adapter<InfoAdapter.ViewHolder>{

    private ArrayList<Info> alinfo;
    private Context context;

    public InfoAdapter(ArrayList<Info> alinfo, Context context) {
        this.alinfo = alinfo;
        this.context = context;
    }


    @NonNull
    @Override
    public ViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.item,parent,false);
        return new ViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ViewHolder holder, int position) {
//        holder.img.setText(String.valueOf(alinfo.get(position).getId()));
//        holder.ID.setText(String.valueOf(alinfo.get(position).getName()));
//        holder.ID.setText(String.valueOf(alinfo.get(position).getPhone()));
        Glide.with(context).load(alinfo.get(position).getPic()).into(holder.img);
        holder.textView.setText(alinfo.get(position).getName());
        holder.textView2.setText(alinfo.get(position).getId());
        holder.txtSDT.setText(alinfo.get(position).getPhone());

        holder.setItemClickListener(new ItemClickListener() {
            @Override
            public void onItemClick(View v, int position) {
                Toast.makeText(context, "Đang chọn item " + (holder.getAdapterPosition() + 1), Toast.LENGTH_SHORT).show();
                Intent intent = new Intent(context, MainActivity2.class);
                intent.putExtra("id",alinfo.get(position).getId());
                intent.putExtra("name",alinfo.get(position).getName());
                intent.putExtra("address",alinfo.get(position).getAddress());
                intent.putExtra("que",alinfo.get(position).getQueQuan());
                intent.putExtra("national",alinfo.get(position).getNational());
                intent.putExtra("date",alinfo.get(position).getDateOfBirth());
                intent.putExtra("sex",alinfo.get(position).getSex());
                intent.putExtra("pic",alinfo.get(position).getPic());
                context.startActivity(intent);
            }
        });
    }

    @Override
    public int getItemCount() {
        return alinfo.size();
    }

    public static class ViewHolder extends RecyclerView.ViewHolder implements View.OnClickListener{
        public ItemClickListener itemClickListener;
        public ImageView img;
        public TextView textView, textView2, txtSDT;

        public ViewHolder(@NonNull View itemView) {
            super(itemView);
            img = itemView.findViewById(R.id.imageView);
            textView = itemView.findViewById(R.id.textView);
            textView2 = itemView.findViewById(R.id.textView2);
            txtSDT = itemView.findViewById(R.id.txtSDT);
            itemView.setOnClickListener(this);
        }

        @Override
        public void onClick(View view) {
            this.itemClickListener.onItemClick(view, getAdapterPosition());
        }

        public void setItemClickListener(ItemClickListener itemClickListener) {
            this.itemClickListener = itemClickListener;
        }
    }
}

package com.tris.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.tris.myapplication.Object.Info;

public class MainActivity2 extends AppCompatActivity {

    private TextView txtId, textViewHoTen, textViewSN, textViewSex, textViewQT, textViewQQ, textViewTT;
    private ImageView imageView3;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);

        txtId = findViewById(R.id.txtId);
        textViewHoTen = findViewById(R.id.textView10);
        textViewSN = findViewById(R.id.textView13);
        textViewSex = findViewById(R.id.textView12);
        textViewQT = findViewById(R.id.textView19);
        textViewQQ = findViewById(R.id.textView17);
        textViewTT = findViewById(R.id.textView18);
        imageView3 = findViewById(R.id.imageView3);

        Intent intent = getIntent();
        Info info = new Info(intent.getStringExtra("id"),intent.getStringExtra("date"),intent.getStringExtra("sex"),intent.getStringExtra("national"),
                intent.getStringExtra("que"),intent.getStringExtra("address"),intent.getStringExtra("name"),null,intent.getStringExtra("pic"));

        txtId.setText(info.getId());
        textViewHoTen.setText(info.getName());
        textViewSN.setText(info.getDateOfBirth());
        textViewSex.setText(info.getSex());
        textViewQT.setText(info.getNational());
        textViewQQ.setText(info.getQueQuan());
        textViewTT.setText(info.getAddress());
        Glide.with(this).load(info.getPic()).into(imageView3);

    }
}
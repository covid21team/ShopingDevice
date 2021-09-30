package com.tris.myapplication;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;

import com.tris.myapplication.Object.Info;

public class MainActivity2 extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main2);

        Intent intent = getIntent();
        String id  = intent.getStringExtra("id");
        String name  = intent.getStringExtra("name");
        String address  = intent.getStringExtra("address");
        String que  = intent.getStringExtra("que");
        String national  = intent.getStringExtra("national");
        String date  = intent.getStringExtra("date");
        String sex  = intent.getStringExtra("sex");
        String pic  = intent.getStringExtra("pic");

        Info info = new Info(intent.getStringExtra("id"),intent.getStringExtra("date"),intent.getStringExtra("sex"),intent.getStringExtra("national"),
                intent.getStringExtra("que"),intent.getStringExtra("address"),intent.getStringExtra("name"),null,intent.getStringExtra("pic"));





    }
}
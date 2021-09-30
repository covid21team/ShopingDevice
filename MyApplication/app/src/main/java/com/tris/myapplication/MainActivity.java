package com.tris.myapplication;

import androidx.appcompat.app.AppCompatActivity;
import androidx.recyclerview.widget.LinearLayoutManager;
import androidx.recyclerview.widget.RecyclerView;

import android.os.Bundle;
import android.widget.ImageView;

import com.tris.myapplication.Object.Info;

import java.util.ArrayList;

public class MainActivity extends AppCompatActivity {
    private RecyclerView rclList;
    private ArrayList<Info> alInfo;
    private InfoAdapter infoAdapter;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        rclList = findViewById(R.id.rclInfo);

        alInfo = new ArrayList<>();

        Info t0 = new Info("1111111111111", "10/10/2000", "Nam", "Việt Nam", "Hà Tĩnh", "Tân Châu - Tây Ninh", "Nguyễn Quang SỸ", "0999999999", "https://api.covid21tsp.space/Picture/personal.png");
        alInfo.add(t0);
        Info t1 = new Info("2222222222222", "11/05/2000", "Nam", "Việt Nam", "Đồng Nai", "Long Khánh - Đồng Nai", "Nguyễn Hoàng Trí", "0988888888", "https://api.covid21tsp.space/Picture/personal.png");
        alInfo.add(t1);
        Info t2 = new Info("3333333333333", "25/12/2000", "Nam", "Việt Nam", "An Giang", "Chợ Mới - An Giang", "Trương Gia Phú", "0977777777", "https://api.covid21tsp.space/Picture/personal.png");
        alInfo.add(t2);
        Info t3 = new Info("4444444444444", "14/02/2000", "Nam", "Việt Nam", "Cần Thơ", "Vĩnh Thạnh - Cần Thơ", "Nguyễn Ngọc Tính", "0966666666", "https://api.covid21tsp.space/Picture/personal.png");
        alInfo.add(t3);

        rclList.setHasFixedSize(true);

        infoAdapter = new InfoAdapter(alInfo, MainActivity.this);

        LinearLayoutManager linearLayoutManager =
                new LinearLayoutManager(this,LinearLayoutManager.VERTICAL,false);
        rclList.setLayoutManager(linearLayoutManager);
        rclList.setAdapter(infoAdapter);

    }
}
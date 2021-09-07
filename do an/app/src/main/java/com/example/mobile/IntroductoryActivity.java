package com.example.mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.widget.ImageView;

import com.airbnb.lottie.LottieAnimationView;

public class IntroductoryActivity extends AppCompatActivity {

    ImageView logo,app_name,splashImg;
    LottieAnimationView lottieAnimationView;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);


        setContentView(R.layout.nen);
        logo = findViewById(R.id.logo);
        app_name = findViewById(R.id.app_name);
        splashImg = findViewById(R.id.img);
        lottieAnimationView = findViewById(R.id.lottie);

        splashImg.animate().translationY(-3500).setDuration(1000).setStartDelay(4000);
        logo.animate().translationY(2400).setDuration(1000).setStartDelay(4000);
        app_name.animate().translationY(2400).setDuration(1000).setStartDelay(4000);
        lottieAnimationView.animate().translationY(2400).setDuration(1000).setStartDelay(4000);
    }
}
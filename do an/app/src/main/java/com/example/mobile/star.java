package com.example.mobile;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.Button;
import android.widget.RatingBar;
import android.widget.TextView;

import java.util.ArrayList;
import java.util.List;

public class star extends AppCompatActivity {
    private RatingBar ratingBarYours;
    private RatingBar ratingBarAll;

    private Button buttonSubmit;

    private TextView textViewYourCurrentRating;
    private TextView textViewRatingCount;
    private TextView textViewSumAllRating;
    private TextView textViewAverageAllRating;

    private List<Float> allRatings = new ArrayList<Float>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_star);




        this.buttonSubmit = (Button) this.findViewById(R.id.button_submit);
        this.ratingBarYours = (RatingBar) this.findViewById(R.id.ratingBar_yours);
        this.ratingBarAll = (RatingBar) this.findViewById(R.id.ratingBar_all);

        this.textViewYourCurrentRating = (TextView) this.findViewById(R.id.textView_yourCurrentRating);
        this.textViewRatingCount= (TextView) this.findViewById(R.id.textView_ratingCount);
        this.textViewSumAllRating= (TextView) this.findViewById(R.id.textView_sumAllRating);
        this.textViewAverageAllRating= (TextView) this.findViewById(R.id.textView_averageAllRating);

        this.ratingBarYours.setOnRatingBarChangeListener(new RatingBar.OnRatingBarChangeListener() {
            @Override
            public void onRatingChanged(RatingBar ratingBar, float rating, boolean fromUser) {
                textViewYourCurrentRating.setText("Your current Rating: " + rating);
            }
        });
        this.buttonSubmit.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                doSubmit();
            }
        });
    }

    private void doSubmit()  {
        float rating = this.ratingBarYours.getRating();
        this.allRatings.add(rating);

        int ratingCount = this.allRatings.size();
        float ratingSum = 0f;
        for(Float r: this.allRatings)  {
            ratingSum += r;
        }
        float averageRating = ratingSum / ratingCount;


        this.textViewRatingCount.setText("Rating Count: " + ratingCount);
        this.textViewSumAllRating.setText("Sum off all Rating: " + ratingSum);
        this.textViewAverageAllRating.setText("Average value off all Rating: " + averageRating);

        float ratingAll = this.ratingBarAll.getNumStars() * averageRating / this.ratingBarYours.getNumStars() ;
        this.ratingBarAll.setRating(ratingAll);
    }
}
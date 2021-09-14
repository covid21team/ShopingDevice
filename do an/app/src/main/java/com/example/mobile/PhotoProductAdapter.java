package com.example.mobile;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;

import androidx.annotation.NonNull;
import androidx.viewpager.widget.PagerAdapter;

import com.bumptech.glide.Glide;

import java.util.List;

public class PhotoProductAdapter extends PagerAdapter {
    public PhotoProductAdapter(Context mContext, List<PhotoProduct> mListPhotoProduct) {
        this.mContext = mContext;
        this.mListPhotoProduct = mListPhotoProduct;
    }

    private Context mContext;
    private List<PhotoProduct> mListPhotoProduct;

    @NonNull
    @Override
    public Object instantiateItem(@NonNull ViewGroup container, int position) {
        View view = LayoutInflater.from(container.getContext()).inflate(R.layout.item_product,container,false);
        ImageView imgPhoto = view.findViewById(R.id.img_photo);

        PhotoProduct photo = mListPhotoProduct.get(position);
        if(photo != null){
            Glide.with(mContext).load(photo.getId()).into(imgPhoto);
        }
        container.addView(view);
        return view;
    }

    @Override
    public int getCount() {
        if(mListPhotoProduct != null){
            return mListPhotoProduct.size();
        }
        return 0;
    }

    @Override
    public boolean isViewFromObject(@NonNull View view, @NonNull Object object) {
        return view == object;
    }

    @Override
    public void destroyItem(@NonNull ViewGroup container, int position, @NonNull Object object) {
        container.removeView((View) object);
    }
}

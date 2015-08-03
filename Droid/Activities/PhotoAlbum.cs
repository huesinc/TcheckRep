﻿using System;

namespace RentProof.Droid
{
    // Photo: contains image resource ID and caption:
    public class Photo
    {
        // Caption text for this photo:
        public string mCaption;
        // Photo ID for this photo:
        public int mPhotoID;
        // Return the ID of the photo:
        public int PhotoID
        {
            get { return mPhotoID; }
        }

        // Return the Caption of the photo:
        public string Caption
        {
            get { return mCaption; }
        }
    }

    // Photo album: holds image resource IDs and caption:
    public class PhotoAlbum
    {
        // Built-in photo collection - this could be replaced with
        // a photo database:

        private static readonly Photo[] mBuiltInPhotos =
        {
            new Photo
            {
                mPhotoID = Resource.Drawable.sampleEsperian,
                mCaption = "Sample Report"
            },
            new Photo
            {
                mPhotoID = Resource.Drawable.sampleEsperian,
                mCaption = "Sample Report"
            },
            new Photo
            {
                mPhotoID = Resource.Drawable.sampleEsperian,
                mCaption = "Sample Report"
            },
            new Photo
            {
                mPhotoID = Resource.Drawable.sampleEsperian,
                mCaption = "Sample Report"
            }
        };

        // Array of photos that make up the album:
        private readonly Photo[] mPhotos;
        // Random number generator for shuffling the photos:
        private readonly Random mRandom;
        // Create an instance copy of the built-in photo list and
        // create the random number generator:
        public PhotoAlbum()
        {
            mPhotos = mBuiltInPhotos;
            mRandom = new Random();
        }

        // Return the number of photos in the photo album:
        public int NumPhotos
        {
            get { return mPhotos.Length; }
        }

        // Indexer (read only) for accessing a photo:
        public Photo this[int i]
        {
            get { return mPhotos[i]; }
        }

        // Pick a random photo and swap it with the top:
        public int RandomSwap()
        {
            // Save the photo at the top:
            var tmpPhoto = mPhotos[0];

            // Generate a next random index between 1 and 
            // Length (noninclusive):
            var rnd = mRandom.Next(1, mPhotos.Length);

            // Exchange top photo with randomly-chosen photo:
            mPhotos[0] = mPhotos[rnd];
            mPhotos[rnd] = tmpPhoto;

            // Return the index of which photo was swapped with the top:
            return rnd;
        }

        // Shuffle the order of the photos:
        public void Shuffle()
        {
            // Use the Fisher-Yates shuffle algorithm:
            for (var idx = 0; idx < mPhotos.Length; ++idx)
            {
                // Save the photo at idx:
                var tmpPhoto = mPhotos[idx];

                // Generate a next random index between idx (inclusive) and 
                // Length (noninclusive):
                var rnd = mRandom.Next(idx, mPhotos.Length);

                // Exchange photo at idx with randomly-chosen (later) photo:
                mPhotos[idx] = mPhotos[rnd];
                mPhotos[rnd] = tmpPhoto;
            }
        }
    }
}
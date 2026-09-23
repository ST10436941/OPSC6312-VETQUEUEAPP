package com.vetqueue.app.data.local

import android.content.Context
import androidx.room.Database
import androidx.room.Room
import androidx.room.RoomDatabase
import com.vetqueue.app.data.local.dao.*
import com.vetqueue.app.data.local.entity.*

@Database(
    entities = [
        UserEntity::class,
        PetEntity::class,
        ClinicEntity::class,
        AppointmentEntity::class,
        QueueEntryEntity::class,
        ReminderEntity::class,
        NotificationEntity::class,
        AchievementEntity::class,
        UserAchievementEntity::class
    ],
    version = 1,
    exportSchema = false
)
abstract class AppDatabase : RoomDatabase() {
    abstract fun userDao(): UserDao
    abstract fun petDao(): PetDao
    abstract fun clinicDao(): ClinicDao
    abstract fun appointmentDao(): AppointmentDao
    abstract fun queueDao(): QueueDao
    abstract fun reminderDao(): ReminderDao
    abstract fun notificationDao(): NotificationDao
    abstract fun achievementDao(): AchievementDao

    companion object {
        @Volatile private var INSTANCE: AppDatabase? = null

        fun getInstance(context: Context): AppDatabase =
            INSTANCE ?: synchronized(this) {
                INSTANCE ?: Room.databaseBuilder(
                    context.applicationContext,
                    AppDatabase::class.java,
                    "vetqueue.db"
                )
                    // Prototype only - replace with a real Migration before the final PoE.
                    .fallbackToDestructiveMigration()
                    .build().also { INSTANCE = it }
            }
    }
}

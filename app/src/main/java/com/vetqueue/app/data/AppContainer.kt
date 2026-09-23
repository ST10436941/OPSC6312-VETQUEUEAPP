package com.vetqueue.app.data

import android.content.Context
import com.vetqueue.app.data.local.AppDatabase
import com.vetqueue.app.data.remote.RetrofitClient
import com.vetqueue.app.data.repository.*

/**
 * Lightweight manual dependency container (no Hilt, to keep the prototype
 * simple to open and run in Android Studio without extra setup). ViewModels
 * pull what they need from here via VetQueueApp.
 */
class AppContainer(context: Context) {
    private val db = AppDatabase.getInstance(context)
    private val api = RetrofitClient.api

    val authRepository = AuthRepository(api, db.userDao())
    val petRepository = PetRepository(api, db.petDao())
    val clinicRepository = ClinicRepository(api, db.clinicDao())
    val appointmentRepository = AppointmentRepository(api, db.appointmentDao())
    val queueRepository = QueueRepository(api, db.queueDao(), clinicRepository)
    val notificationRepository = NotificationRepository(db.notificationDao())
    val reminderRepository = ReminderRepository(db.reminderDao())
}

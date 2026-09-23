package com.vetqueue.app.data.local.entity

import androidx.room.Entity
import androidx.room.PrimaryKey

/**
 * Local (Room) mirrors of the ERD in the Part 1 document (Figure 18).
 * These tables back the offline-first Requirement 3.8: writes made offline
 * are stored here with syncStatus = "pending" and pushed to the REST API
 * (see data/remote and data/repository) once connectivity returns.
 */

@Entity(tableName = "users")
data class UserEntity(
    @PrimaryKey val userId: String,
    val firstName: String,
    val surname: String,
    val email: String,
    val passwordHash: String, // "salt:hash" - see util.PasswordUtil, never plain text
    val language: String = "English",
    val notificationsEnabled: Boolean = true
)

@Entity(tableName = "pets")
data class PetEntity(
    @PrimaryKey val petId: String,
    val userId: String,
    val name: String,
    val species: String,
    val breed: String = "",
    val dateOfBirth: String = "",
    val gender: String = "",
    val weight: Double = 0.0,
    val notes: String = "",
    val syncStatus: String = "synced" // pending | synced | retry
)

@Entity(tableName = "clinics")
data class ClinicEntity(
    @PrimaryKey val clinicId: String,
    val name: String,
    val address: String,
    val contact: String,
    val hours: String,
    val services: String, // comma separated - kept simple for the prototype
    val currentQueueCount: Int = 0,
    val estWaitMinutes: Int = 0
)

@Entity(tableName = "appointments")
data class AppointmentEntity(
    @PrimaryKey val appointmentId: String,
    val userId: String,
    val petId: String,
    val clinicId: String,
    val service: String,
    val date: String,
    val time: String,
    val status: String = "upcoming", // upcoming | completed | cancelled
    val syncStatus: String = "pending"
)

@Entity(tableName = "queue_entries")
data class QueueEntryEntity(
    @PrimaryKey val queueId: String,
    val clinicId: String,
    val userId: String,
    val petId: String,
    val queueNumber: Int,
    val position: Int,
    val estWaitMinutes: Int,
    val status: String = "waiting" // waiting | in_progress | left | done
)

@Entity(tableName = "reminders")
data class ReminderEntity(
    @PrimaryKey val reminderId: String,
    val userId: String,
    val petId: String,
    val type: String, // vaccination | checkup | flea_treatment | deworming
    val dueDate: String,
    val status: String = "pending", // pending | done
    val syncStatus: String = "pending"
)

@Entity(tableName = "notifications")
data class NotificationEntity(
    @PrimaryKey val notificationId: String,
    val userId: String,
    val message: String,
    val type: String, // queue_update | reminder | confirmed
    val read: Boolean = false,
    val createdAt: Long = System.currentTimeMillis()
)

@Entity(tableName = "achievements")
data class AchievementEntity(
    @PrimaryKey val achievementId: String,
    val title: String,
    val description: String
)

@Entity(tableName = "user_achievements", primaryKeys = ["userId", "achievementId"])
data class UserAchievementEntity(
    val userId: String,
    val achievementId: String,
    val unlockedAt: Long = System.currentTimeMillis()
)

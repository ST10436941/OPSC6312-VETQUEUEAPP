package com.vetqueue.app.data.local.dao

import androidx.room.*
import com.vetqueue.app.data.local.entity.*
import kotlinx.coroutines.flow.Flow

@Dao
interface UserDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(user: UserEntity)

    @Query("SELECT * FROM users WHERE email = :email LIMIT 1")
    suspend fun findByEmail(email: String): UserEntity?

    @Query("SELECT * FROM users WHERE userId = :userId LIMIT 1")
    suspend fun findById(userId: String): UserEntity?

    @Query("UPDATE users SET language = :language WHERE userId = :userId")
    suspend fun updateLanguage(userId: String, language: String)

    @Query("UPDATE users SET notificationsEnabled = :enabled WHERE userId = :userId")
    suspend fun updateNotifications(userId: String, enabled: Boolean)

    @Query("UPDATE users SET firstName = :firstName, surname = :surname WHERE userId = :userId")
    suspend fun updateProfile(userId: String, firstName: String, surname: String)
}

@Dao
interface PetDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(pet: PetEntity)

    @Update
    suspend fun update(pet: PetEntity)

    @Delete
    suspend fun delete(pet: PetEntity)

    @Query("SELECT * FROM pets WHERE userId = :userId ORDER BY name")
    fun observeByUser(userId: String): Flow<List<PetEntity>>

    @Query("SELECT * FROM pets WHERE petId = :petId LIMIT 1")
    suspend fun findById(petId: String): PetEntity?

    @Query("SELECT * FROM pets WHERE syncStatus = 'pending'")
    suspend fun getPendingSync(): List<PetEntity>
}

@Dao
interface ClinicDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertAll(clinics: List<ClinicEntity>)

    @Query("SELECT * FROM clinics ORDER BY name")
    fun observeAll(): Flow<List<ClinicEntity>>

    @Query("SELECT * FROM clinics WHERE name LIKE '%' || :query || '%'")
    fun search(query: String): Flow<List<ClinicEntity>>

    @Query("SELECT * FROM clinics WHERE clinicId = :clinicId LIMIT 1")
    suspend fun findById(clinicId: String): ClinicEntity?
}

@Dao
interface AppointmentDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(appointment: AppointmentEntity)

    @Update
    suspend fun update(appointment: AppointmentEntity)

    @Query("SELECT * FROM appointments WHERE userId = :userId ORDER BY date, time")
    fun observeByUser(userId: String): Flow<List<AppointmentEntity>>

    @Query("SELECT * FROM appointments WHERE clinicId = :clinicId AND date = :date AND time = :time AND status = 'upcoming'")
    suspend fun findConflict(clinicId: String, date: String, time: String): AppointmentEntity?

    @Query("SELECT * FROM appointments WHERE syncStatus = 'pending'")
    suspend fun getPendingSync(): List<AppointmentEntity>
}

@Dao
interface QueueDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(entry: QueueEntryEntity)

    @Update
    suspend fun update(entry: QueueEntryEntity)

    @Query("SELECT * FROM queue_entries WHERE userId = :userId AND status = 'waiting' LIMIT 1")
    fun observeActiveForUser(userId: String): Flow<QueueEntryEntity?>

    @Query("SELECT COUNT(*) FROM queue_entries WHERE clinicId = :clinicId AND status = 'waiting'")
    suspend fun countWaiting(clinicId: String): Int
}

@Dao
interface ReminderDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(reminder: ReminderEntity)

    @Update
    suspend fun update(reminder: ReminderEntity)

    @Query("SELECT * FROM reminders WHERE userId = :userId ORDER BY dueDate")
    fun observeByUser(userId: String): Flow<List<ReminderEntity>>

    @Query("SELECT * FROM reminders WHERE syncStatus = 'pending'")
    suspend fun getPendingSync(): List<ReminderEntity>
}

@Dao
interface NotificationDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insert(notification: NotificationEntity)

    @Query("SELECT * FROM notifications WHERE userId = :userId ORDER BY createdAt DESC")
    fun observeByUser(userId: String): Flow<List<NotificationEntity>>

    @Query("UPDATE notifications SET read = 1 WHERE notificationId = :id")
    suspend fun markRead(id: String)

    @Query("UPDATE notifications SET read = 1 WHERE userId = :userId")
    suspend fun markAllRead(userId: String)
}

@Dao
interface AchievementDao {
    @Insert(onConflict = OnConflictStrategy.REPLACE)
    suspend fun insertAll(achievements: List<AchievementEntity>)

    @Query("SELECT * FROM achievements")
    fun observeAll(): Flow<List<AchievementEntity>>

    @Insert(onConflict = OnConflictStrategy.IGNORE)
    suspend fun unlock(userAchievement: UserAchievementEntity)

    @Query("SELECT achievementId FROM user_achievements WHERE userId = :userId")
    fun observeUnlockedIds(userId: String): Flow<List<String>>
}

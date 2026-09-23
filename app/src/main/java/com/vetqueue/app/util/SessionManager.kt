package com.vetqueue.app.util

import android.content.Context
import androidx.datastore.preferences.core.edit
import androidx.datastore.preferences.core.stringPreferencesKey
import androidx.datastore.preferences.preferencesDataStore
import kotlinx.coroutines.flow.Flow
import kotlinx.coroutines.flow.first
import kotlinx.coroutines.flow.map

private val Context.dataStore by preferencesDataStore(name = "vetqueue_session")

/**
 * Persists the logged-in user's id/email and app preferences (language,
 * notifications) across app restarts, so Profile & Settings changes survive
 * process death - covers Requirement 3.2 (User Settings).
 */
class SessionManager(private val context: Context) {

    companion object {
        val KEY_USER_ID = stringPreferencesKey("user_id")
        val KEY_USER_EMAIL = stringPreferencesKey("user_email")
        val KEY_USER_NAME = stringPreferencesKey("user_name")
        val KEY_LANGUAGE = stringPreferencesKey("language")
        val KEY_NOTIFICATIONS = stringPreferencesKey("notifications_enabled")
    }

    val userIdFlow: Flow<String?> = context.dataStore.data.map { it[KEY_USER_ID] }
    val userNameFlow: Flow<String?> = context.dataStore.data.map { it[KEY_USER_NAME] }
    val languageFlow: Flow<String> = context.dataStore.data.map { it[KEY_LANGUAGE] ?: "English" }
    val notificationsFlow: Flow<Boolean> =
        context.dataStore.data.map { (it[KEY_NOTIFICATIONS] ?: "true").toBoolean() }

    suspend fun currentUserId(): String? = context.dataStore.data.first()[KEY_USER_ID]

    suspend fun login(userId: String, email: String, name: String) {
        context.dataStore.edit {
            it[KEY_USER_ID] = userId
            it[KEY_USER_EMAIL] = email
            it[KEY_USER_NAME] = name
        }
    }

    suspend fun logout() {
        context.dataStore.edit { it.clear() }
    }

    suspend fun setLanguage(language: String) {
        context.dataStore.edit { it[KEY_LANGUAGE] = language }
    }

    suspend fun setNotificationsEnabled(enabled: Boolean) {
        context.dataStore.edit { it[KEY_NOTIFICATIONS] = enabled.toString() }
    }
}

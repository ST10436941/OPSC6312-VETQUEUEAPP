package com.vetqueue.app

import android.os.Bundle
import androidx.activity.ComponentActivity
import androidx.activity.compose.setContent
import androidx.compose.foundation.layout.fillMaxSize
import androidx.compose.material3.Surface
import androidx.compose.ui.Modifier
import com.vetqueue.app.ui.navigation.VetQueueNavHost
import com.vetqueue.app.ui.theme.VetQueueTheme

class MainActivity : ComponentActivity() {
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContent {
            VetQueueTheme {
                Surface(modifier = Modifier.fillMaxSize()) {
                    VetQueueNavHost()
                }
            }
        }
    }
}

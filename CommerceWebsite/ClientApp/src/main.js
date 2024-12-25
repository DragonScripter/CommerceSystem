import { createApp } from 'vue'
import App from './App.vue'
import router from './router';
App.use(router);
createApp(App).mount('#app')

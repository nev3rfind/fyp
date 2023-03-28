import { createApp } from 'vue';
import 'bootstrap';

// load all solid icons
// modify here to load individual icons as needed to reduce bundle size
import { fas } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
library.add(fas);

import Layout from "../components/Layout";
import NavigationBar from "../components/NavigationBar";
import HomePage from "../components/HomePage";

// pull in main stylesheet
require('../sass/app.scss');

const app = createApp({
    components: {
        Layout,
        NavigationBar,
        HomePage
    }
});

app.component('font-awesome-icon', FontAwesomeIcon);

app.mount('#app');

if (module.hot) {
    module.hot.accept();
}

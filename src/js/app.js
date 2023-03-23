import { createApp } from 'vue';

// load all solid icons
// modify here to load individual icons as needed to reduce bundle size
import { fas } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
library.add(fas);

import HelloWorld from "../components/HelloWorld";
import Layout from "../components/Layout";

// pull in main stylesheet
require('../sass/app.scss');

const app = createApp({
    components: {
        HelloWorld,
        Layout
    }
});

app.component('font-awesome-icon', FontAwesomeIcon);

app.mount('#app');

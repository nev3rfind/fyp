import { createApp } from 'vue';
import router from './router';
import 'bootstrap';

// load all solid icons
// modify here to load individual icons as needed to reduce bundle size
import { fas } from '@fortawesome/free-solid-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
library.add(fas);

import App from "../components/App";

// pull in main stylesheet
require('../sass/app.scss');

const app = createApp({
    components: {
        App,
    }
}).use(router);

app.component('font-awesome-icon', FontAwesomeIcon);

app.mount('#app');

if (module.hot) {
    module.hot.accept();
}

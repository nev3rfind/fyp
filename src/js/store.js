import { createStore } from "vuex";

const store = createStore({
    state: {
        user: null,
    },
    mutations: {
        setUser(state, { user, isFullyAuth }) {
            if (user) {
                user.isFullyAuth = isFullyAuth;
            }
            state.user = user;
        },
    },
    getters: {
        isLoggedIn(state) {
            return state.user !== null;
        },
    },
});

export default store;

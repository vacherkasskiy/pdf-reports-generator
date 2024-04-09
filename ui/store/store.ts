import {combineReducers, configureStore} from '@reduxjs/toolkit'
import {reportsApi} from "@/api/ReportsApi";

const rootReducer = combineReducers({
    [reportsApi.reducerPath]: reportsApi.reducer,
})

const setupStore = () => {
    return configureStore({
        reducer: rootReducer,
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(reportsApi.middleware)
    })
}

// для типизирования useSelector, useDispatch
export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']

export default setupStore
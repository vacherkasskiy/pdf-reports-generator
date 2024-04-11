import {combineReducers, configureStore} from '@reduxjs/toolkit'
import {reportsApi} from "@/api/services/ReportsApi";
import {reportApi} from "@/api/services/ReportApi";

const rootReducer = combineReducers({
    [reportsApi.reducerPath]: reportsApi.reducer,
    [reportApi.reducerPath]: reportApi.reducer,
})

const setupStore = () => {
    return configureStore({
        reducer: rootReducer,
        middleware: (getDefaultMiddleware) =>
            getDefaultMiddleware().concat(
                reportsApi.middleware,
                reportApi.middleware
            )
    })
}

// для типизирования useSelector, useDispatch
export type RootState = ReturnType<typeof rootReducer>
export type AppStore = ReturnType<typeof setupStore>
export type AppDispatch = AppStore['dispatch']

export default setupStore
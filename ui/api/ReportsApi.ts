import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import {Report} from "@/models";
import {AddReportRequest} from "@/api/requests";

export const reportsApi = createApi({
    reducerPath: 'reportsApi',
    baseQuery: fetchBaseQuery({baseUrl: 'https://localhost:7090'}),
    tagTypes: ['Reports'],
    endpoints: (build) => ({
        fetchReports: build.query<Report[], void>({
            query: () => ({
                url: '/app/v1/reports',
                method: 'GET',
                // credentials: 'include',
            }),
            providesTags: ['Reports'],
        }),
        addReport: build.mutation<void, AddReportRequest>({
            query: (request: AddReportRequest) => ({
                url: '/reports',
                method: 'POST',
                body: request,
                // credentials: 'include',
            }),
            invalidatesTags: ['Reports'],
        })
    }),
})

export const {
    // useAddReportMutation,
    useFetchReportsQuery
} = reportsApi
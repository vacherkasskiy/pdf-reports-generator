import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react'
import config from '../../config.json';
import {CreateReportRequest} from "@/api/requests";

export const reportApi = createApi({
    reducerPath: 'reportApi',
    baseQuery: fetchBaseQuery({baseUrl: config.apiReportsBaseUrl}),
    tagTypes: ['Reports'],
    endpoints: (build) => ({
        createReport: build.mutation<void, CreateReportRequest | undefined>({
            query: (requestBody: CreateReportRequest) => ({
                url: `/`,
                method: 'POST',
                body: requestBody,
                headers: {
                    'Content-Type': 'application/json'
                },
            }),
            invalidatesTags: ['Reports'],
        }),
    }),
})

export const {
    useCreateReportMutation,
} = reportApi
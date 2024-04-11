'use client';

import {Inter} from "next/font/google";
import "./globals.css";
import "./null.css";
import React from "react";
import {Provider} from "react-redux";
import setupStore from "@/store/store";

const inter = Inter({subsets: ["latin"]});

interface RootLayoutProps {
    children: React.ReactNode,
}

const store = setupStore()

export default function RootLayout({children}: RootLayoutProps) {
    return (
        <html lang="en">
        <body className={inter.className}>
        <Provider store={store}>
            {children}
        </Provider>
        </body>
        </html>
    );
}

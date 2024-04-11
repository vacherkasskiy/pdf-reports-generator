'use client';

import styles from "./page.module.scss";
import ReportsPageContainer from "@/components/ReportsPage/ReportsPage/ReportsPageContainer";

export default function Home() {
  return (
    <main className={styles.main}>
      <ReportsPageContainer />
    </main>
  );
}

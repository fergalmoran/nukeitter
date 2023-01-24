"use client";
import { useSession } from "next-auth/react";
import { LoginPage, LoggedInPage } from "@/components/pages";
import { LoadingSpinner } from "@/components/widgets";

export default function Home() {
  const session = useSession();
  const _getPage = () => {
    switch (session.status) {
      case "authenticated":
        return <LoggedInPage />;
      case "loading":
        return <LoadingSpinner />;
      case "unauthenticated":
        return <LoginPage />;
    }
  };
  return (
    <div className="container flex flex-col items-center mx-auto my-12 md:flex-row md:my-24">
      {_getPage()}
    </div>
  );
}

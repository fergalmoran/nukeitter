"use client";
import NavbarComponent from "@/components/layout/NavBar";
import { SessionProvider } from "next-auth/react";
import "./globals.css";

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en" data-theme="cupcake">
      <head />
      <body>
        <SessionProvider>
          <NavbarComponent />

          <main>
            <article className="px-4 content">{children}</article>
          </main>
        </SessionProvider>
      </body>
    </html>
  );
}

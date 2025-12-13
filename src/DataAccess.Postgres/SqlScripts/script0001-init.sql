CREATE EXTENSION IF NOT EXISTS pgcrypto;

CREATE TABLE IF NOT EXISTS users (
    name          text PRIMARY KEY,
    password_hash text NOT NULL
);

CREATE TABLE IF NOT EXISTS notes (
    id         uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    owner_id   text NOT NULL,
    created_at timestamptz NOT NULL DEFAULT now(),
    content    text NOT NULL,

    CONSTRAINT fk_notes_users_owner_id
        FOREIGN KEY (owner_id)
        REFERENCES users (name)
        ON DELETE CASCADE
);

CREATE INDEX IF NOT EXISTS ix_notes_owner_created_at
    ON notes(owner_id, created_at DESC);